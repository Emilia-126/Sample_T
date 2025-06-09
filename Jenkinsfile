pipeline {
    agent any
    options {
        timestamps() 
    }
    environment {
        GIT_BRANCH = env.BRANCH_NAME ?: 'main' 
 	echo "GIT_BRANCH = ${GIT_BRANCH}"
    }	
    tools {
        msbuild 'MSBuild_2019'
    }
    stages {
        stage('Cleanup Workspace') {
            steps {
                cleanWs()  // 清除 Jenkins 工作區
            }
        }
        stage('Checkout') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Checkout..."
                    //git(url: 'https://github.com/Emilia-126/Sample_T.git', branch: 'main')
		     git branch: "${GIT_BRANCH}", credentialsId: 'github_SSH', url: 'https://github.com/Emilia-126/Sample_T.git'
                    def endTime = System.currentTimeMillis()
                    echo "Checkout【 ${GIT_BRANCH} 】耗時: ${(endTime - startTime) / 1000} 秒"
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
                expression { GIT_BRANCH == 'main' }
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
                expression { GIT_BRANCH == 'main' }
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
