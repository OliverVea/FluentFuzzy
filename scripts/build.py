import subprocess

from constants import SLN_FILE

args = ['dotnet', 'build', SLN_FILE]

process = subprocess.Popen(args)

exit(process.exitcode)