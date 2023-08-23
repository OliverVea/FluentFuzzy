from subprocess import Popen

from _root import repository_root

dotnet_test = ['dotnet', 'test', str(repository_root / 'test' / 'FluentFuzzy.Test')]

process = Popen(dotnet_test)
process.wait()

exit(process.returncode)