pipeline {
    agent any
    options {
        timestamps() 
    }	
    tools {
        msbuild 'MSBuild_2019'
    }
    stages {
    	stage('Setup Environment') {
            steps {
                script {
                    env.GIT_BRANCH = env.BRANCH_NAME ?: 'main'
                    echo "Using branch 【 ${env.GIT_BRANCH} 】"
                }
            }
        }
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
                    git(url: 'https://github.com/Emilia-126/Sample_T.git', branch: 'main')
		     //git branch: "${env.GIT_BRANCH}", url: 'https://github.com/Emilia-126/Sample_T.git'
                    def endTime = System.currentTimeMillis()
                    echo "Checkout【 "${env.GIT_BRANCH}" 】耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    def startTime = System.currentTimeMillis()
                    echo "開始 Build..."
		    catchError(buildResult: 'SUCCESS', stageResult: 'FAILURE') {
			bat '"C:\\Program Files\\Microsoft Visual Studio\\2019\\Enterprise\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe" ConsoleApp1.exe'
			error("Unit tests failed!")
		     }	
		    //bat 'msbuild TestDTSeqEqual.sln /p:Configuration=Release %MSBUILD_ARGS%'
                    def endTime = System.currentTimeMillis()
                    echo "Build 耗時: ${(endTime - startTime) / 1000} 秒"
                }
            }
        }
        stage('Test') {
	    when {
                expression { env.GIT_BRANCH == 'main' }
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
                expression { env.GIT_BRANCH == 'main' }
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
