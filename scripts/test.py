from subprocess import Popen

from constants import TEST_ROOT

test_projects = TEST_ROOT.glob('**/*.Test.csproj')

for project in test_projects:
    dotnet_test = ['dotnet', 'test', str(project)]

    process = Popen(dotnet_test)
    process.wait()
