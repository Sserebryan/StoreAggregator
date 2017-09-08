import * as React from "react";
import styled, { withProps, ThemeProvider } from "../../../styled-components";
import { IMenu, IProps, IUserAction } from "../../../helper";

interface NavProps {
  menu: IMenu[];
  className?: string;
  userActions: IUserAction[];
}

const StyledNav = withProps(styled.nav)`
    display: flex;
    margin: 0;
    padding: 0;
    justify-content: flex-start;

    list-style: none;
`;

interface LinkProps {
  link: string;
}

const StyledNavLink = withProps<LinkProps, HTMLAnchorElement>(
  styled.a.attrs({
    href: "#",
    alt: "Logo"
  })
)`
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    border-right: 1px solid rgba(255,255,255,0.1);
    color: white;
    transition: transform 0.2s;
    border-bottom: 5px solid transparent;
    background-color: #43593f;
    font-size: 16px;
    padding: 1.2rem 2rem 1rem 2rem;

    & img {
        transition: all 0.2s;
        margin-bottom: 0.5rem;
        width: 40px;
    }

    & span {
        font-size: 16px;
    }

    &:hover {
      border-bottom-color: rgba(0,0,0,0.2);
      border-right-color: rgba(0,0,0,0.05);
      color: white;
      & img {
        transform: scale(1.2);
      }

      background: linear-gradient(135deg, red, blue);
    }
`;

const StyledLogoLink = StyledNavLink.extend`padding: 0;`;

const StyledLogo = withProps(styled.li)`
    display: flex;
    & img{
        width: 200px;
        margin: 0;
    }
`;

const Logo: React.StatelessComponent<any> = props => (
  <StyledLogo>
    <StyledLogoLink link="#">
      <img src="images/icons/logo.svg" alt="Logo" />
    </StyledLogoLink>
  </StyledLogo>
);

const StyledMenuItem = withProps(styled.li)`
    display: flex;
    flex-direction: column;
`;

const MenuItem: React.StatelessComponent<IMenu> = props => (
  <StyledMenuItem>
    <StyledNavLink link={props.slug}>
      <img src={"images/icons/" + props.icon} height="40px" />
      <span>{props.title}</span>
    </StyledNavLink>
  </StyledMenuItem>
);

const StyledSearchItem = withProps<any, HTMLInputElement>(
  styled.input.attrs({
    type: "text",
    name: "Search",
    placeholder: "Coffee, bar and...."
  })
)`   font-size: 30px;
    color: black;
    outline: 0;
    border: 0;
    width: 100%;
    background-color: #43593f;
`;

const SearchItem: React.StatelessComponent = props => <StyledSearchItem />;

const StyledUserActionItem = withProps(styled.li)`
    background-color: #43593f;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    font-size: 1rem;
    padding: 0 1rem;
    border-right: 1px solid rgba(255,255,255,0.1);
    & a{
        color: white;
    }
`;

const UserActionItem: React.StatelessComponent<IUserAction> = props => (
  <StyledUserActionItem>
    <a href={props.slug}>
      <span>{props.title}</span>
    </a>
  </StyledUserActionItem>
);

const Nav: React.StatelessComponent<NavProps> = props => (
  <StyledNav>
    <Logo />
    {props.menu.map(m => (
      <MenuItem key={m.key} slug={m.slug} title={m.title} icon={m.icon} />
    ))}
    <SearchItem />
    {props.userActions
      .filter(x => x.isRegistered)
      .map(m => (
        <UserActionItem
          slug={m.slug}
          key={m.key}
          title={m.title}
          isRegistered={m.isRegistered}
        />
      ))}
  </StyledNav>
);

export default Nav;
