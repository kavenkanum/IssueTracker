import React, { useState, Fragment } from 'react';
import {
    Container,
    Image,
    Menu,
    Visibility
} from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { isUserLogged, getUserFullName } from './../../features/users/selectors';

const menuStyle = {
    border: 'none',
    borderRadius: 0,
    boxShadow: 'none',
    marginBottom: '1em',
    transition: 'box-shadow 0.5s ease, padding 0.5s ease',
}

const fixedMenuStyle = {
    backgroundColor: '#fff',
    border: '1px solid #ddd',
    boxShadow: '0px 3px 5px rgba(0, 0, 0, 0.2)',
}

interface AppHeaderProps {
    logo: any
}

export const AppHeader: React.FC<AppHeaderProps> = (props: AppHeaderProps) => {
    const [topMenuFixed, setTopMenuFixed] = useState(false);
    const isLoggedIn = useSelector(isUserLogged);
    const userFullName = useSelector(getUserFullName);

    return <Visibility
        onBottomPassed={() => setTopMenuFixed(true)}
        onBottomVisible={() => setTopMenuFixed(false)}
        once={false}>
        <Menu borderless
            fixed={topMenuFixed ? 'top' : undefined}
            style={topMenuFixed ? fixedMenuStyle : menuStyle}>
            <Container text>
                <Menu.Item>
                    <Image size='mini' src={props.logo} as={Link} to="/" />
                </Menu.Item>
                <Menu.Item header>
                    Scrooge
                </Menu.Item>
                <Menu.Menu position='right'>
                    {isLoggedIn ?
                        (
                            <Fragment>
                                <Menu.Item>{userFullName}</Menu.Item>
                                <Menu.Item as={Link} to="/logout">Logout</Menu.Item>
                            </Fragment>
                        ) : (
                            <Fragment>
                                <Menu.Item as={Link} to="/register">Register</Menu.Item>
                                <Menu.Item as={Link} to="/login">Login</Menu.Item>
                            </Fragment>
                        )}
                </Menu.Menu>
            </Container>
        </Menu>
    </Visibility>
}