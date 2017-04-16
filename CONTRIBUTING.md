# Contributing

We love pull requests from everyone. We're in a major pivot where we restarted from scratch. It's
a rough time but a good one to start contributing.

Fork, then clone the repo:

```shell
git clone https://github.com/your-username/parle
```

Set up your dev environment:

1. You'll need Go all setup in your $GOPATH
2. You'll need node
3. You'll need webpack and gulp

```
cd parle
npm install
npm install -g webpack gulp
```

Make your change, there's no unit test yet [something that one could do]

Push to your fork and [submit a pull request](https://github.com/parle-io/parle/compare/).

At this point you're waiting on us. We like to at least comment on pull requests
within three business days (and, typically, one business day). We may suggest
some changes or improvements or alternatives.

Some things that will increase the chance that your pull request is accepted:

* Write tests.
* Follow our [style guide](https://github.com/parle-io/parle).
* Write a [good commit message](https://github.com/parle-io/parle).

Some ideas on what you could start with:

* Unit tests
* CSS styling for the widget to make it pretty
* Logo for this project
* Using some easy way to batch insert messages instead of writing them in realtime.
