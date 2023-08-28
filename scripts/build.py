import subprocess

from constants import SRC_ROOT

projects = SRC_ROOT.glob('**/*.csproj')

for project in projects:

    if 'unity' in str(project).lower():
        continue

    args = ['dotnet', 'build', project]

    process = subprocess.Popen(args)
    result = process.wait()

    if result:
        exit(result)