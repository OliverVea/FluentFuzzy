import dataclasses
import pathlib

from _root import repository_root

@dataclasses.dataclass
class Project:
    name: str
    path: pathlib.Path
    csproj: str

    def get_csproj_path(self) -> str:
        return self.path / self.csproj

PROJECTS = [
    Project('FluentFuzzy', repository_root / 'src' / 'FluentFuzzy', 'FluentFuzzy.csproj'),
    Project('FluentFuzzy.Visualization', repository_root / 'src' / 'FluentFuzzy.Visualization', 'FuzzyLogic.Visualization.csproj')
]

def get_project_names() -> list[str]:
    return [project.name for project in PROJECTS]

def get_project(project_name: str) -> Project:
    return next((project for project in PROJECTS if project.name == project_name), None)