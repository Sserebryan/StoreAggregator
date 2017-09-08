export const siteName = `Now That's Delicious!`;

export interface IProps{
  key: string;
}

export interface IMenuItem{
    slug: string;
    title: string;
}

export interface IMenu extends IProps, IMenuItem{
    
    icon: string;
}

export interface IUserAction extends IMenuItem, IProps{
  isRegistered: boolean;
}

export const userActions = [
  { slug: '/account', title: 'Account', key: '1', isRegistered: true},
  { slug: '/logout', title: 'Logout', key: '2', isRegistered: true},
  { slug: '/register', title: 'Register', key: '3', isRegistered: false},
  { slug: '/login', title: 'Login', key: '4', isRegistered: false}
] as IUserAction[];

export const menu = [
  { slug: '/stores', title: 'Stores', icon: 'store.svg', key: '1'},
  { slug: '/tags', title: 'Tags', icon: 'tag.svg', key: '2'},
  { slug: '/top', title: 'Top', icon: 'top.svg', key: '3'},
  { slug: '/add', title: 'Add', icon: 'add.svg', key: '4'},
  { slug: '/map', title: 'Map', icon: 'map.svg', key: '5'},
]  as IMenu[];
