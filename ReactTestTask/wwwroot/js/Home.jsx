class UserBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: this.props.initialData };
        this.handleUserSubmit = this.handleUserSubmit.bind(this);
    }
    handleUserSubmit(user) {
        const data = new FormData();
        data.append('UserId', user.UserId);
        data.append('DateRegestration', user.DateRegestration);
        data.append('DateLastVisit', user.DateLastVisit);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = () => this.loadUsersFromServer();
        xhr.send(data);

    }
    loadUsersFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    componentDidMount() {
        window.setInterval(
            () => this.loadUsersFromServer(),
            this.props.pollInterval,
        );
    }
    render() {
        return (
            <div className="userBox">
                <h2>Список пользователей</h2>
                <UserList data={this.state.data} />
                <UserForm onUserSubmit={this.handleUserSubmit} />
            </div>
        );
    }
}

class UserList extends React.Component {
    render() {
        const usersNodes = this.props.data.map(user => (
            <User key={user.userId} userId={user.userId} dateRegestration={user.dateRegestration} dateLastVisit={user.dateLastVisit} />
        ));
        return (
            <div className="userList">
                <table className="userListTable">
                    <thead>
                        <tr>
                            <th>UserID</th>
                            <th>Date Regestration</th>
                            <th>Date Last Visit</th>
                        </tr>
                    </thead>
                    <tbody>
                        {usersNodes}
                    </tbody>
                </table>
            </div>
        );
    }
}

class User extends React.Component {
    render() {
        return (
            <tr>
                <td>{this.props.userId}</td>
                <td>{this.props.dateRegestration}</td>
                <td>{this.props.dateLastVisit}</td>
            </tr>
        );
    }
}

class UserForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { id: '', dateRegestration: '', dateLastVisit: '' };
        this.handleIdChange = this.handleIdChange.bind(this);
        this.handleDateRegestrationChange = this.handleDateRegestrationChange.bind(this);
        this.handleDateLastVisitChange = this.handleDateLastVisitChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleIdChange(e) {
        this.setState({ id: e.target.value });
    }
    handleDateRegestrationChange(e) {
        this.setState({ dateRegestration: e.target.value });
    }
    handleDateLastVisitChange(e) {
        this.setState({ dateLastVisit: e.target.value });
    }
    handleSubmit(e) {
        e.preventDefault();
        const id = this.state.id;
        const dateRegestration = this.state.dateRegestration;
        const dateLastVisit = this.state.dateLastVisit;
        if (!id || !dateRegestration || !dateLastVisit) {
            return;
        }
        this.props.onUserSubmit({ UserId: id, DateRegestration: dateRegestration, DateLastVisit: dateLastVisit });
        this.setState({ id: '', dateRegestration: '', dateLastVisit: '' });
    }
    render() {
        return (
            <form className="userForm" onSubmit={this.handleSubmit} >
                <input
                    type="number"
                    placeholder="Put ID here"
                    value={this.state.id}
                    onChange={this.handleIdChange}
                />
                <input
                    type="date"
                    value={this.state.dateRegestration}
                    onChange={this.handleDateRegestrationChange}
                />
                <input
                    type="date"
                    value={this.state.dateLastVisit}
                    onChange={this.handleDateLastVisitChange}
                />
                <input type="submit" value="Post"/>
            </form>
        );
    }
}