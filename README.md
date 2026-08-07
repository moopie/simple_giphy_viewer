# A Simple Giphy Viewer

The point of this app is to:

- Get trending gifs
- Search gifs

Using the Giphy open API

## Usage

To use this app you need to set your Giphy API key

```bash
$ dotnet user-secrets set "Giphy:ApiKey" "your-api-key" --project src/SimpleGiphyViewer.Api
```

To launch the backend you need to `cd` to `src/SimpleGiphyViewer.Api` and start
the backend server

```bash
$ cd src/SimpleGiphyViewer.Api/
$ dotnet run
```

For the web interface you need to `cd` to the `web` directory and start
the frontend

```bash
$ cd web/
$ npm run dev
```

## AI Disclosure

LLM was used during the planning phase, generation of the Giphy contract types,
CSS and github actions
