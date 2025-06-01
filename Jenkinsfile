pipeline {
    agent any
    options {
        timestamps() // 在輸出中加上時間戳
    }
    stages {
        stage('Checkout') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Checkout..."
                    git(url: 'https://github.com/Emilia-126/Sample_T.git', branch: 'main', changelog: true)
                    def endTime = System.currentTimeMillis()
                    echo "Checkout 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Build..."
					bat 'msbuild TestDTSeqEqual.sln /p:Configuration=Release /p:Platform="Any CPU"'
                    def endTime = System.currentTimeMillis()
                    echo "Build 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Test') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Test..."
                    bat 'vstest.console.exe YourTestProject.dll'
                    def endTime = System.currentTimeMillis()
                    echo "Test 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
    }
    post {
        always {
            script {
                echo "總執行時間: ${currentBuild.duration / 1000} 秒"
            }
        }
    }
}
