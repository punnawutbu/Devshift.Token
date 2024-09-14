def folderName = 'Devshift.Token'
def projectName = 'Devshift.Token'
def gitUrl = 'https://github.com/punnawutbu/Devshift.Token.git'
def gitBranch = 'refs/heads/master'
def publishProject = 'Devshift.Token/Devshift.Token.csproj'
def testProject = 'Devshift.Token.Tests'
def versionPrefix = "1.0"

folder(projectName)
pipelineJob("$projectName/Release") {
  logRotator(-1, 10)
  triggers {
    upstream("$projectName/Seed", 'SUCCESS')
  }
  definition {
    parameters {
      choiceParam('Release', ['Beta', 'General Availability (GA)'], '')
    }
    cps {
      sandbox()
      script("""
        @Library('jenkins-shared-libraries')_

        def _versionSuffix = Release == 'Beta' ? 'beta' : ''

        nuGetV2 {
          projectName = '$projectName'
          gitUri = '$gitUrl'
          gitBranch = '$gitBranch'
          publishProject = '$publishProject'
          testProject = '$testProject'
          versionPrefix = '$versionPrefix'
          versionSuffix = _versionSuffix
        }
     """)
    }
  }
}