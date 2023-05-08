# Intro to Functional Programming in C#

## Useful Commands

Run build:

```sh
$ dotnet build
```

Run tests:

```sh
$ dotnet test
# or with file watcher
$ dotnet watch test
```

## Demo Web API

Add an item
```sh
curl -X 'POST' \
  'http://localhost:5023/item' \
  -H 'Content-Type: application/json' \
  -d '{ "name": "foo", "qty": 100 }'
# response: OK 33a2bd17-37ff-4caa-937f-9258d7a4c47e
```

Check in items
```sh
curl -X 'POST' \
  'http://localhost:5023/item/checkin' \
  -H 'Content-Type: application/json' \
  -d '{ "id": "33a2bd17-37ff-4caa-937f-9258d7a4c47e", "qty": 10 }'
# response: OK
```

Get an item
```sh
curl -X 'GET' \
  'http://localhost:5023/item/33a2bd17-37ff-4caa-937f-9258d7a4c47e'
# response: OK {"id":"e8345cea-68fc-4a37-bc4f-2cc308f92838","name":"foo","qty":110}
```

## Documentation

- [Church-encoding in C#](https://qfpl.io/posts/fp-in-csharp/)
- [C# Exhaustive Checking (Pattern Match)](https://github.com/WalkerCodeRanger/ExhaustiveMatching)
- [LanguageExt](https://github.com/louthy/language-ext)
- [LanguageExt - API](https://louthy.github.io/language-ext/LanguageExt.Core/index.html)
