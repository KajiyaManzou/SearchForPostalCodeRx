# SearchForPostalCodeRx

`SearchForPostalCodeRx` は、日本郵便が提供する郵便番号検索 API（ZipCloud）を利用し、入力した郵便番号に基づいて住所をリアクティブに検索する Blazor WebAssembly 製の PWA です。MudBlazor と System.Reactive を組み合わせ、入力のデバウンスや UI 更新を滑らかにしています。

## 主な機能
- 7 桁の郵便番号をリアルタイムに監視し、自動的に住所を検索
- MudBlazor ベースのマテリアルデザイン UI
- ZipCloud API から取得した住所の表示
- PWA 対応（オフラインキャッシュ、ホーム画面追加、アイコン）
- GitHub Pages への自動デプロイ（GitHub Actions）

## デモ
- GitHub Pages: https://kajiyamanzou.github.io/SearchForPostalCodeRx/
  - 初回アクセス時に HTTPS 環境で開くと、PWA としてインストール可能になります。

## 技術スタック
- .NET 8 / Blazor WebAssembly
- MudBlazor 8.x
- System.Reactive 6.x
- ZipCloud API（https://zipcloud.ibsnet.co.jp/doc/api）
- GitHub Actions（pages ブランチへのデプロイ）

## セットアップ
### 必要環境
- .NET SDK 8.0 以上

### ローカル実行手順
1. リポジトリをクローンします。
   ```bash
   git clone https://github.com/KajiyaManzou/SearchForPostalCodeRx.git
   cd SearchForPostalCodeRx
   ```
2. 依存関係を復元します。
   ```bash
   dotnet restore SearchForPostalCodeRx/SearchForPostalCodeRx.sln
   ```
3. 開発サーバーを起動します（ホットリロード対応）。
   ```bash
   dotnet watch --project SearchForPostalCodeRx/src/SearchForPostalCodeRx.App run
   ```
4. ブラウザで `https://localhost:7194`（dotnet watch の出力に表示される URL）にアクセスします。

### テスト
現在、自動テストは未整備です。必要に応じて `tests` ディレクトリ配下にプロジェクトを追加してください。

## ディレクトリ構成（抜粋）
```
SearchForPostalCodeRx/
├─ SearchForPostalCodeRx.sln
├─ src/
│  └─ SearchForPostalCodeRx.App/
│     ├─ Pages/                     # Blazor ページ（SearchPostalCode.razor など）
│     ├─ wwwroot/
│     │  ├─ index.html              # PWA 設定、サービスワーカー登録
│     │  ├─ manifest.json           # PWA マニフェスト
│     │  └─ service-worker.js       # 基本的な Cache First 戦略
│     └─ SearchForPostalCodeRx.App.csproj
└─ tests/                           # テストプロジェクト追加用の場所
```

## PWA 対応のポイント
- `wwwroot/manifest.json` でアプリ名・アイコン・テーマカラーを定義
- `wwwroot/service-worker.js` でルートページや CSS、Blazor ランタイムを Cache First でキャッシュ
- `wwwroot/index.html` にマニフェストリンクとサービスワーカー登録スクリプトを追加
- `.csproj` に `ServiceWorkerAssetsManifest` とサービスワーカーの発行設定を追加

## CI/CD（GitHub Pages デプロイ）
- `.github/workflows/deplay-to-gh-pages.yml`
  - `main` ブランチへの push でビルドと公開を自動実行
  - Pull Request ではビルドのみを行い、Pages 関連ステップはスキップ
  - 発行済みの `wwwroot` を GitHub Pages にアップロード

## 外部 API について
- 本アプリは ZipCloud API を利用しています。
- API は商用利用不可（2025 年 2 月時点）などの制限があるため、利用規約を必ず確認してください。
- 短時間で大量のリクエストを発生させると制限にかかる可能性があります。

## よくある質問
- **郵便番号はハイフン付きでも検索できますか？**
  - ハイフンなし 7 桁を対象としています。ハイフンは自動フォーマットで表示時に挿入されます。
- **オフラインでも検索できますか？**
  - オフライン時は直近で取得したキャッシュは表示されますが、新規検索は API が利用できないため動作しません。

## ライセンス
- このプロジェクトはMITライセンスの下で公開されています。
