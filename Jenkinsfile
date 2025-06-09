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
                    //git(url: 'https://github.com/Emilia-126/Sample_T.git', branch: 'main')
		 	git branch: env.BRANCH_NAME, credentialsId: 'github_SSH', url: 'https://github.com/Emilia-126/Sample_T.git'
                    def endTime = System.currentTimeMillis()
                    echo "Checkout【 ${env.BRANCH_NAME} 】耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Build..."
		    //bat 'msbuild TestDTSeqEqual.sln /p:Configuration=Release %MSBUILD_ARGS%'
			bat "msbuild TestDTSeqEqual.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /m"
                    def endTime = System.currentTimeMillis()
                    echo "Build 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Test') {
	    when {
                expression { env.BRANCH_NAME == 'main'}
            }
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
                expression { env.BRANCH_NAME == 'main'}
            }
            steps {
	    	script {
			def startTime = System.currentTimeMillis()
	    		echo "開始 Deploy..." 
                	bat 'msdeploy -source:package.zip -dest:auto'
	    		def endTime = System.currentTimeMillis()
	    		echo "Test 耗時: ${(endTime - startTime) / 1000} 秒"
	    	}
            }
        }
    	
    }
    post {
        always {
            script {
		cleanWs()  // 清除 Jenkins 工作區
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
