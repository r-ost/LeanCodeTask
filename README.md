# LeanCodeTask

Application connects to Reddit api to get images.

Available api endpoints:
- /random - fetches random image url from top 100 hot posts in a given subbreddit
- /history - fetches downloaded images history


## Run the application with docker-compose

**Linux**
```shell
export RedditApi__ClientSecret="<Your Reddit api client secret>"
docker-compose up -d --build
```

**Windows**
```shell
$env:RedditApi__ClientSecret = "<Your Reddit api client secret>"
docker-compose up -d --build
```

Application runs on http://localhost:8080


## Configure subbreddit

By default application uses _r/poland_ subbreddit. In order to change the subrreddit
you have to change the value in _appsettings.json_.
```
"RedditApi": {
    ...
    "Subreddit": "<type subreddit name e.g. bitcoin>"
    ...
}
```