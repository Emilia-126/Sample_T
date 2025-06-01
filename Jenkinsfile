pipeline {
  agent any
  stages {
    stage('test') {
      steps {
        echo 'start'
      }
    }

    stage('checkout') {
      steps {
        git(url: 'https://github.com/Emilia-126/Sample_T.git', branch: 'main', changelog: true)
      }
    }

  }
}