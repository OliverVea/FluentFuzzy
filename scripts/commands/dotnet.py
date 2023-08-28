import pathlib
import subprocess

from constants import SOURCE_NAME, NAMESPACE

def _run(args, allow_failed: bool = False):
    process = subprocess.Popen(args)

    result = process.wait()

    if allow_failed:
        return 0

    if result != 0:
        exit(result)


def pack(project: pathlib.Path,
        out: pathlib.Path,
        configuration: str,
        include_symbols: bool = False,
        include_source: bool = False,
        version_suffix: str | None = None):
    """`dotnet pack`: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack"""

    args = ['dotnet', 'pack']
    args += [str(project)]
    args += ['--configuration', configuration]
    if include_source:
        args += ['--include-source']
    if include_symbols:
        args += ['--include-symbols']
    if version_suffix is not None:
        args += ['--version-suffix', version_suffix]
    args += ['--output', str(out)]

    return _run(args)

def nuget_add_source(username: str, token: str):
    """`dotnet nuget add source`: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-add-source"""

    args = ['dotnet', 'nuget', 'add', 'source',
            '--username', username,
            '--password', token,
            '--store-password-in-clear-text',
            '--name', SOURCE_NAME,
            f'https://nuget.pkg.github.com/{NAMESPACE}/index.json']
    
    return _run(args, allow_failed = True)

def nuget_push(package_file: pathlib.Path, token: str):
    """`dotnet nuget push`: """

    args = ['dotnet', 'nuget', 'push', str(package_file),
            '--api-key', token,
            '--source', SOURCE_NAME]
    
    return _run(args)

# dotnet_nuget = ['dotnet', 'nuget', 'push', str(out / '*.nupkg'),
#                 '--api-key', args.nuget_api_key,
#                 '--source', 'https://api.nuget.org/v3/index.json']

# process = Popen(dotnet_nuget, stdout=pipe, stderr=pipe)
# process.wait()




# dotnet_nuget_symbols = ['dotnet', 'nuget', 'push', str(out / '*.snupkg'),
#                 '--api-key', args.nuget_api_key]

# process = Popen(dotnet_nuget_symbols, stdout=pipe, stderr=pipe)
# process.wait()