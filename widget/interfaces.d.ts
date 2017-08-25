interface IParle {
	token: string;
	templates: { [key: string]: Function };
	attributes: IConfiguration;
	state: IState;
	events: { [key: string]: (e: MouseEvent) => void };

	eventHandlers: Array<IEventHandlers>;

	init: (attributes: IConfiguration) => void;
	load: () => void;
	open: () => void;
	close: () => void;
	listConvos: (conversations: Array<IConversation>) => void;

	parseTemplate: (html: string) => Function;
	renderDiscussions: () => void;

	send: (evt: "identify"|"listconv"|"newconv", data: any) => void;
}

interface IConfiguration { }

interface IState {
	isOpen: boolean;
	conversations?: Array<IConversation>;
}

interface IParleEvent {
	token: string;
	name: string;
	data: string;
}

interface IEventHandlers {
	id: string;
	event: string;
	handler: EventListenerOrEventListenerObject;
}

interface IConversation {
	id: string;
	created: Date;
	userID: string;
	messages: Array<IMessage>;
	isClosed: boolean;
}

interface IMessage {
	id: string;
	first: string;
	last: string;
	email: string;
	body: string;
	sentOn: Date;
}
