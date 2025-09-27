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