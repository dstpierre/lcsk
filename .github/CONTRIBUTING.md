# Contributing to Parle

👍🎉 Thank you for your interest in contributing to Parle 🎉👍

Before you begin, make sure to familiarize yourself with the 
[Code of Conduct](https://github.com/parle-io/parle/blob/master/CODE_OF_CONDUCT.md). If you've 
previously contributed to other open source project, you may recognize it as the classic 
[Contributor Covenant](http://contributor-covenant.org/).

If you want to chat with the team or the community, you can 
[join our #parle channel on freenode](https://webchat.freenode.net/).

## Issues

When opening an issue:

* include the full **stack trace** with your error
* include your Parle setup
* list versions you are using: Parle, elm, browser, OS, etc.

It's always better to include more info rather than less.

## Code

It's always best to open an issue before investing a lot of time into a
fix or new functionality.  Functionality must meet my design goals and
vision for the project to be accepted; I would be happy to discuss how
your idea can best fit into Parle.

We love pull requests from everyone. We're in a major pivot where we restarted from scratch. It's
a rough time but a good one to start contributing.

Fork, then clone the repo:

```shell
git clone https://github.com/your-username/parle
```

Set up your dev environment:

```shell
$ npm install -g elm
$ npm install -g elm-live elm-format
```

```
cd parle
elm make src/Main.elm --output=dist/app.js
```

You can run the app with `elm-live` like this:

```shell
elm-live src/Main.elm -u -- --output=dist/app.js
```

Make your change, there's no unit test yet [something that one could do]

Push to your fork and [submit a pull request](https://github.com/parle-io/parle/compare/).

At this point you're waiting on us. We like to at least comment on pull requests
within three business days (and, typically, one business day). We may suggest
some changes or improvements or alternatives.

Some things that will increase the chance that your pull request is accepted:

* Write tests.
* Use elm-format to make sure we all have same code format.
* Write a good commit message and PR description.

Some ideas on what you could start with:

* Unit tests
* Any UI and screen creation
* Pick one aspect (Dashboard, Conversation, Campaign, Ticket, Users/Companies) and talk to us on what can be done.


## Legal

By submitting a Pull Request, you disavow any rights or claims to any changes
submitted to the Parle project and assign the copyright of
those changes to Focus Centric inc.

If you cannot or do not want to reassign those rights (your employment
contract for your employer may not allow this), you should not submit a PR.
Open an issue and someone else can do the work.

This is a legal way of saying "If you submit a PR to us, that code becomes ours".
99.9% of the time that's what you intend anyways; we hope it doesn't scare you
away from contributing.

## Financial contributions

We also welcome financial contributions in full transparency on our [open collective](https://opencollective.com/parle).
Anyone can file an expense. If the expense makes sense for the development of the community, it will be "merged" in the ledger of our open collective by the core contributors and the person who filed the expense will be reimbursed.

## Credits 

### Contributors

Thank you to all the people who have already contributed to parle!
<a href="graphs/contributors"><img src="https://opencollective.com/parle/contributors.svg?width=890" /></a>


### Backers

Thank you to all our backers! [[Become a backer](https://opencollective.com/parle#backer)]

<a href="https://opencollective.com/parle#backers" target="_blank"><img src="https://opencollective.com/parle/backers.svg?width=890"></a>


### Sponsors

Thank you to all our sponsors! (please ask your company to also support this open source project by [becoming a sponsor](https://opencollective.com/parle#sponsor))

<a href="https://opencollective.com/parle/sponsor/0/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/0/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/1/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/1/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/2/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/2/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/3/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/3/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/4/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/4/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/5/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/5/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/6/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/6/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/7/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/7/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/8/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/8/avatar.svg"></a>
<a href="https://opencollective.com/parle/sponsor/9/website" target="_blank"><img src="https://opencollective.com/parle/sponsor/9/avatar.svg"></a>