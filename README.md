# NATS by Example

NATS by Exampleは、NATSメッセージングシステムの主要なパターンを示すサンプルコード集です。このリポジトリでは以下のメッセージングパターンを実装した具体的なコードを提供します。

- Core Publish-Subscribe
- Request-Reply
- Queue Groups

---

## サンプルコードの詳細

### 1. Core Publish-Subscribe

このサンプルは、NATSにおける基本的な**Publish-Subscribe（パブリッシュ・サブスクライブ）**モデルを実装しています。

#### Publisherの処理内容
1. **NATSサーバーへの接続**:
   Publisherは`NATS.Client`ライブラリを使用してNATSサーバーに接続します。
2. **メッセージの送信**:
   特定のトピック（例: `"example.topic"`）に対してメッセージをパブリッシュします。送信するメッセージは文字列（例: `"Hello, NATS!"`）で構成されています。
3. **終了処理**:
   処理が完了するとNATSの接続をクローズします。

#### Subscriberの処理内容
1. **NATSサーバーへの接続**:
   Subscriberは`NATS.Client`を使用してNATSサーバーに接続します。
2. **トピックのサブスクライブ**:
   指定されたトピック（例: `"example.topic"`）を購読し、そのトピックに送信されるすべてのメッセージを受信します。
3. **メッセージの処理**:
   受信したメッセージをコンソールに出力します。

コード例：
- [Publisher](https://github.com/SoundOrion/NATSbyExample/blob/main/Messaging/Core%20Publish-Subscribe/Publisher/Program.cs)
- [Subscriber](https://github.com/SoundOrion/NATSbyExample/blob/main/Messaging/Core%20Publish-Subscribe/Subscriber/Program.cs)

---

### 2. Request-Reply

このサンプルでは、NATSの**Request-Reply（リクエスト・リプライ）**モデルを実装しています。リクエストを送信し、それに対するレスポンスを受信する方法を示します。

#### Publisher (Requester) の処理内容
1. **NATSサーバーへの接続**:
   Requesterは`NATS.Client`を使用してNATSサーバーに接続します。
2. **リクエストの送信**:
   特定のトピック（例: `"example.request"`）に対してリクエストを送信します。
3. **レスポンスの受信**:
   サーバー（Responder）から返されるレスポンスを受信し、コンソールに表示します。
   - レスポンス例: `"Response received: Hello from Responder!"`

#### Subscriber (Responder) の処理内容
1. **NATSサーバーへの接続**:
   Responderは`NATS.Client`を使用してNATSサーバーに接続します。
2. **リクエストの処理**:
   特定のトピック（例: `"example.request"`）をリッスンし、リクエストを受信します。
   - 受信したリクエスト内容をコンソールに表示します。
3. **レスポンスの送信**:
   リクエストを処理した後、レスポンスをリクエスト元に返します。レスポンスの内容は静的な文字列（例: `"Hello from Responder!"`）として設定されています。

コード例：
- [Publisher](https://github.com/SoundOrion/NATSbyExample/blob/main/Messaging/Request-Reply/Publisher/Program.cs)
- [Subscriber](https://github.com/SoundOrion/NATSbyExample/blob/main/Messaging/Request-Reply/Subscriber/Program.cs)

---

### 3. Queue Groups

このサンプルでは、NATSの**Queue Groups（キューグループ）**を利用したメッセージングを実装しています。キューグループに属するサブスクライバー間でメッセージをロードバランシングする方法を示します。

#### Publisherの処理内容
1. **NATSサーバーへの接続**:
   Publisherは`NATS.Client`を使用してNATSサーバーに接続します。
2. **メッセージの送信**:
   指定されたトピック（例: `"example.queue"`）に対してメッセージを送信します。
   - メッセージは文字列（例: `"Message for queue group"`）として送信されます。

#### Subscriberの処理内容
1. **NATSサーバーへの接続**:
   Subscriberは`NATS.Client`を使用してNATSサーバーに接続します。
2. **キューグループへの参加**:
   複数のSubscriberが同じキューグループに参加することで、メッセージのロードバランシングを実現します。
   - グループ名はコード内で定義されています（例: `"example-group"`）。
3. **メッセージの受信と処理**:
   メッセージは同じグループ内の1つのSubscriberにのみ配信され、受信したSubscriberはメッセージをコンソールに出力します。

コード例：
- [Publisher](https://github.com/SoundOrion/NATSbyExample/blob/main/Messaging/Queue%20Groups/Publisher/Program.cs)
- [Subscriber](https://github.com/SoundOrion/NATSbyExample/blob/main/Messaging/Queue%20Groups/Subscriber/Program.cs)

---

## 注意点

各サンプルコードは、NATSサーバーが起動している状態で動作します。NATSサーバーのセットアップ方法や起動コマンドについては、[NATS公式ドキュメント](https://nats.io/documentation/)をご参照ください。