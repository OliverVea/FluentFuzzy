import subprocess

from constants import SLN_FILE

args = ['dotnet', 'build', SLN_FILE]

process = subprocess.Popen(args)
result = process.wait()

exit(result)