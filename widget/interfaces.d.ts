interface IParle {
	templates: { [key: string]: Function };
	attributes: IConfiguration;
	state: IState;
	events: { [key: string]: (e: MouseEvent) => void };

	init: (attributes: IConfiguration) => void;
	load: () => void;
	open: () => void;
	close: () => void;
	parseTemplate: (html: string) => Function;
}

interface IConfiguration { }

interface IState {
	isOpen: boolean;
}