# サンプル（プロダクト名）

[![IMAGE ALT TEXT HERE](https://jphacks.com/wp-content/uploads/2020/09/JPHACKS2020_ogp.jpg)](https://www.youtube.com/watch?v=G5rULR53uMk)

## 製品概要
### 背景(製品開発のきっかけ、課題等）
### 製品説明（具体的な製品の説明）
### 特長
####1. 特長1
####2. 特長2
####3. 特長3

### 解決出来ること
### 今後の展望
### 注力したこと（こだわり等）
* 
* 

## 開発技術
### 活用した技術
#### API・データ
* Todoist API
* Google Cloud Text-to-Speech API
* Google Cloud Speech-to-Text API
* 身近な人に匿名で「日常生活のストレス」についてのアンケート結果

#### フレームワーク・ライブラリ・モジュール
* バンダイナムコ研究所様提供 感情判定Adapter
* バックエンド
  * インフラ: Google App Engine
  * ミドルウェア: Gunicorn (Google App Engine)
  * サーバサイド: Python, Flask
* クライアント
  * Unity: C#

### 独自技術
#### ハッカソンで開発した独自機能・技術
* 送られたメッセージをバンダイナムコ研究所様提供の感情判定Adapterを用いて返すメッセージを変化させた
* TodoistAPIを用いてポジティブなメッセージのときにリマインド
* TodoistAPIのOAuthで認証
