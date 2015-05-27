Unity-Template
==============

自分のUnityプロジェクトのテンプレート

## Testフォルダ内のスクリプトを動作させるには、Unity Test Tools が必要です。  
また、スクリプトを有効にするには、Scripting Define Symbols等で"Ksnm_EnableUnitTest"を定義してください。
Unity Test Toolsを導入しない環境では、"Ksnm_EnableUnitTest"を定義しないことで、エラーが発生しないようにしています。

## LitJson について

Assets/Ksnm/LitJson は、その名の通りパブリックドメインの LitJson(0.9.0)を元に作成しました。  
元のLitJsonから変更が加えられて同一の動作ではなくなったため、Ksnm名前空間の下に配置しています。

LitJsonのオリジナル:http://lbv.github.io/litjson/

