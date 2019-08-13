# License README

Parle is licensed as "source-available" software rather than "Open Source" (as 
defined by the Open Source Initiative).

The purpose of this document is to provide some rationale behind this decision 
and answer some common questions that may arise.

## A quick tour of licensing

There are several common licensing schemes (and accompanying business models) 
for software businesses:

- Closed-source and proprietary
- The "open core" model: an open-source core version with a dual-licensed proprietary offering (e.g., GitLab)
- Open source with a managed services component (e.g., Discourse and Ghost)
- Source-available (similar to "open source" but with some added restrictions, such as the Commons Clause)

A majority choose to adopt the closed-source, proprietary model. The calculus 
is pretty straightforward: the company funds the development of the product and 
retains all intellectual property rights to monetize it.

The open core and fully open source models open up more possibilities: people 
from the community can download and use it for free and can contribute their 
development efforts back to the project if they so choose. There are many 
officially "approved" open source licenses with varying properties that all 
adhere to [The Open Source Definition](https://opensource.org/osd) defined by 
the Open Source Initiative.

By and large, companies licensing their code as open source rely on selling 
hosting (or other managed services) to generate revenue. Additionally, most 
reserve their trademarks/copyrights to prevent a competing business from 
marketing their codebase under the same name (or something confusingly similar).

## Why source-available is right for Parle

After publishing my [book on Go and Saas](https://buildsaasappingo.com) I've 
decided to build Parle out in the open as a demonstration of a Go and Elm SaaS.

Ideally my main goal is to build a profitable company to support the continued 
development of the product.

I want to make sure that businesses can freely host their own instance. And also 
offer a fully managed one-click install, hosting, support and monitoring for a 
fee that can keep lights open.

I just don't want to have to compete with someone that could take the codebase 
and start a company offering the exact same product.


There are some ways to disincentivize people from doing this:

- Dual-licensing the codebase and reserving the most valuable features for the proprietary version
- Intentionally making it harder to deploy the codebase (or keep that kind of code private altogether)
- Raising venture capital funding to level the playing field with other well-funded opportunists

None of these options align with my values and my vision for Parle. Rather than 
adopting a 100% permissive license and attempting to play defense against rational 
actors, it makes the most sense for Parle's licensing to align with my original goals.

## How the Commons Clause works

Parle is licensed using a combination of the Apache 2.0 license and the 
[Commons Clause](https://commonsclause.com/), a license condition drafted by 
Heather Meeker. The clause can be added to an existing open source license to 
disallow one particular right: the right to sell a product "whose value derives, 
entirely or substantially, from the functionality of the Software."

This means you can still:

- Host the software for your own personal and commercial use
- Fork the codebase and use it as the basis for a non-competing product or service
- Sell consulting services to help people host Parle for their own use

But, you cannot do the following without permission:

- Sell a hosted (SaaS) version of Parle
- Sell a downloadable version of Parle

I genuinely believe this is the most reasonable and sustainable licensing scheme 
for Parle moving forward.