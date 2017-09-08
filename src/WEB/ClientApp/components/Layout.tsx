import * as React from 'react';
import { NavMenu } from './NavMenu';
import Nav from './Common/Nav';
import {menu, userActions} from '../helper';
import styled, {withProps} from '../styled-components';

const Header = withProps(styled.header)`
    padding: 0;
`;

export class Layout extends React.Component<{}, {}> {
    public render() {
        return <Header>
                <Nav menu = {menu} userActions = {userActions}></Nav>
            </Header>
    }
}


