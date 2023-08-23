import argparse
import shutil
import datetime

from subprocess import Popen, DEVNULL

from _projects import get_project_names, get_project
from _root import repository_root

project_names = get_project_names()

parser = argparse.ArgumentParser()

parser.add_argument('--project', '-p', choices=project_names, required=True)
parser.add_argument('--config', '-c', choices=['Debug', 'Release'], default='Debug')
parser.add_argument('--nuget-api-key', '-k', required=True)

args = parser.parse_args()

project = get_project(args.project)

csproj = str(project.get_csproj_path())
out = repository_root / 'build' / project.name

dotnet_pack = ['dotnet', 'pack', csproj]

if args.config.lower() == 'release':
    dotnet_pack += ['--configuration', 'Release']
    out /= 'release'

elif args.config.lower() == 'debug':
    dotnet_pack += ['--configuration', 'Debug']
    dotnet_pack += ['--include-symbols']
    dotnet_pack += ['--include-source']
    date_suffix = datetime.datetime.now().strftime("%Y-%m-%d-%H-%M-%S")
    dotnet_pack += ['--version-suffix', f'd-{date_suffix}']
    out /= 'debug'

else:
    f'Error: Invalid configuration {args.config}'
    exit()

dotnet_pack += ['--output', str(out)]

pipe = None

if out.exists():
    shutil.rmtree(str(out.absolute()))

process = Popen(dotnet_pack, stdout=pipe, stderr=pipe)
process.wait()

dotnet_nuget = ['dotnet', 'nuget', 'push', str(out / '*.nupkg'),
                '--api-key', args.nuget_api_key,
                '--source', 'https://api.nuget.org/v3/index.json']

process = Popen(dotnet_nuget, stdout=pipe, stderr=pipe)
process.wait()

dotnet_nuget_symbols = ['dotnet', 'nuget', 'push', str(out / '*.snupkg'),
                '--api-key', args.nuget_api_key]

process = Popen(dotnet_nuget_symbols, stdout=pipe, stderr=pipe)
process.wait()