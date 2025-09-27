# PWA化作業記録

## 概要
.NET Blazor WebAssemblyアプリケーション「SearchForPostalCodeRx」をPWA（Progressive Web App）化した作業内容を記録。

## 実施した変更

### 1. プロジェクトファイル設定
**ファイル**: `SearchForPostalCodeRx.App.csproj`

```xml
<PropertyGroup>
  <ServiceWorkerAssetsManifest>service-worker-assets.js</ServiceWorkerAssetsManifest>
</PropertyGroup>

<ItemGroup>
  <ServiceWorker Include="wwwroot\service-worker.js" PublishedContent="wwwroot\service-worker.js" />
</ItemGroup>
```

### 2. マニフェストファイル作成
**ファイル**: `wwwroot/manifest.json`

```json
{
    "name": "SearchForPostalCodeRx",
    "short_name": "PostalCode",
    "start_url": "./",
    "display": "standalone",
    "background_color": "#ffffff",
    "theme_color": "#000000",
    "icons": [
        {
            "src": "icon-192.png",
            "type": "image/png",
            "sizes": "192x192"
        },
        {
            "src": "icon-512.png",
            "type": "image/png",
            "sizes": "512x512"
        }
    ]
}
```

### 3. index.html更新
**ファイル**: `wwwroot/index.html`

追加した要素：
```html
<!-- PWAマニフェスト -->
<link rel="manifest" href="manifest.json" />

<!-- テーマカラー設定 -->
<meta name="theme-color" content="#000000" />

<!-- Apple Touch Icon -->
<link rel="apple-touch-icon" href="icon-192.png" />

<!-- サービスワーカー登録スクリプト -->
<script>
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('./service-worker.js');
    }
</script>
```

### 4. サービスワーカー実装
**ファイル**: `wwwroot/service-worker.js`

```javascript
const CACHE_NAME = 'postal-code-v1';
const urlsToCache = [
    '/',
    '/css/app.css',
    '/css/bootstrap/bootstrap.min.css',
    '/_framework/blazor.webassembly.js'
];

self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => cache.addAll(urlsToCache))
    );
});

self.addEventListener('fetch', event => {
    event.respondWith(
        caches.match(event.request)
            .then(response => response || fetch(event.request))
    );
});
```

### 5. アイコンファイル設定
- `icon-192.png`: 既存ファイルを使用
- `icon-512.png`: 192pxアイコンから複製作成

### 6. テーマカラー統一
- index.html: `#1976d2` → `#000000`
- manifest.json: `#000000`で統一

## 変更されたファイル一覧

### 修正ファイル
- `SearchForPostalCodeRx.App.csproj`
- `wwwroot/index.html`

### 新規作成ファイル
- `wwwroot/manifest.json`
- `wwwroot/service-worker.js`
- `wwwroot/icon-512.png`

## PWA機能

### 実装された機能
1. **インストール可能**: ブラウザの「ホーム画面に追加」でアプリインストール可能
2. **オフライン対応**: 基本的なリソースをキャッシュしてオフライン表示可能
3. **スタンドアローン表示**: ブラウザUIなしでアプリ単体表示
4. **アイコン表示**: ホーム画面やアプリ一覧でカスタムアイコン表示

### キャッシュ戦略
- Cache First: キャッシュ優先、なければネットワークから取得
- 対象リソース: ルートページ、CSS、Blazor WebAssemblyランタイム

## 検証方法

1. アプリケーションをビルド・実行
2. ブラウザでアクセス
3. 開発者ツールのApplicationタブでPWA設定確認
4. ブラウザのインストールプロンプト確認
5. オフライン動作テスト

## 注意事項

- アイコンファイルは512pxが192pxの複製のため、必要に応じて適切なサイズの画像に差し替え推奨
- サービスワーカーのキャッシュ戦略は基本的な実装のため、プロダクション環境では要件に応じてカスタマイズが必要
- HTTPS環境でのみPWA機能が完全に動作

## 完了日時
2025-09-27