pipeline {
    agent any
    options {
        timestamps() 
    }
    tools {
        msbuild 'MSBuild_2019'
    }
    stages {
        stage('Checkout') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Checkout..."
                    git(url: 'https://github.com/Emilia-126/Sample_T.git', branch: 'main')
			//checkout scm
                    def endTime = System.currentTimeMillis()
                    echo "Checkout 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Build') {
	    when {
                branch 'main'
            }
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Build..."
		    //bat 'msbuild TestDTSeqEqual.sln /p:Configuration=Release %MSBUILD_ARGS%'
			bat 'msbuild /t:Rebuild /p:Configuration=Release'
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
                    echo "未執行任何動作"
                    def endTime = System.currentTimeMillis()
                    echo "Test 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
	stage('Deploy') {
            when {
                branch 'main'
            }
            steps {
		def startTime = System.currentTimeMillis()
	    	echo "開始 Deploy..." 
                bat 'msdeploy -source:package.zip -dest:auto'
	    	def endTime = System.currentTimeMillis()
	    	echo "Test 耗時: ${(endTime - startTime) / 1000} 秒"
            }
        }
    	
    }
    post {
        always {
            script {
                echo "Pipeline 總執行時間: ${currentBuild.duration / 1000} 秒"
            }
        }
        success {
            echo 'Pipeline 執行成功 🎉'
        }
        failure {
            echo 'Pipeline 失敗，請檢查錯誤 ❌'
        }
    }
}
