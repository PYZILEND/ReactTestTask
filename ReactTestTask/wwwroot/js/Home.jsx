class UserList extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: this.props.data };
        this.state.data.forEach((data) => {
            data.isValid = true;
        });
        this.handleUsersSubmit = this.handleUsersSubmit.bind(this);
        this.onDateRegestrationChange = this.onDateRegestrationChange.bind(this);
        this.onDateLastVisitChange = this.onDateLastVisitChange.bind(this)
    }

    onDateRegestrationChange(value, index) {
        let items = [...this.state.data];
        let item = { ...items[index] };
        item.dateRegestration = value;
        items[index] = item;
        this.setState({ data: items });
    }

    onDateLastVisitChange(value, index) {
        let items = [...this.state.data];
        let item = { ...items[index] };
        item.dateLastVisit = value;
        items[index] = item;
        this.setState({ data: items });
    }

    handleUsersSubmit(e) {
        e.preventDefault();

        const dataJSON = JSON.stringify(this.state.data);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.postUrl, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onload = () => {
            const response = JSON.parse(xhr.responseText);            
            for (var i = 0; i < this.state.data.length; i++)
            {
                let items = [...this.state.data];
                let item = { ...items[i] };
                item.isValid = response.validityIndexes[i];
                items[i] = item;
                this.setState({ data: items });
            }
        }
        xhr.send(dataJSON);
    }

    render() {
        var index = 0;        
        return (
            <div className="userList">
                <form onSubmit={this.handleUsersSubmit}>
                    <table className="userListTable">
                        <thead>
                            <tr>
                                <th>User ID</th>
                                <th>Date Regestration</th>
                                <th>Date Last Visit</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.data.map(userData => (
                                <User
                                    key={index}
                                    itemIndex={index++}
                                    userId={userData.userId}
                                    dateRegestration={userData.dateRegestration}
                                    dateLastVisit={userData.dateLastVisit}
                                    isValid={userData.isValid}
                                    onDateLastVisitChange={this.onDateLastVisitChange}
                                    onDateRegestrationChange={this.onDateRegestrationChange}
                                />
                            ))}
                        </tbody>
                    </table>
                    <input type="submit" value="Save"/>
                </form>
            </div>
        );
    }
}

class User extends React.Component {
    
    constructor(props) {
        super(props);
        this.handleDateRegestrationChange = this.handleDateRegestrationChange.bind(this);
        this.handleDateLastVisitChange = this.handleDateLastVisitChange.bind(this);
    }

    handleDateRegestrationChange(e) {
        this.props.onDateRegestrationChange(e.target.value, this.props.itemIndex);
    }

    handleDateLastVisitChange(e) {
        this.props.onDateLastVisitChange(e.target.value, this.props.itemIndex);
    }

    render() {
        let newDate = new Date()
        let date = newDate.getDate();
        let month = newDate.getMonth() + 1;
        let year = newDate.getFullYear();

        let maxDate = `${year}-${month < 10 ? `0${month}` : `${month}`}-${date}`
        return (
            <tr>
                <td>
                    {this.props.userId}
                </td>
                <td>
                    <input
                        type="date"
                        min="1970-01-01"
                        max={maxDate}
                        value={this.props.dateRegestration}
                        onChange={this.handleDateRegestrationChange}
                    />
                </td>
                <td>
                    <input
                        type="date"
                        min="1970-01-01"
                        max={maxDate}
                        value={this.props.dateLastVisit}
                        onChange={this.handleDateLastVisitChange}
                    />
                </td>
                                  
                {
                    this.props.isValid == false &&
                    <td className="noBorderCell">  
                    <span>Значение Date Last VIsit должно быть позднее Date Regestration</span>
                    </td>
                }
                
            </tr>
        );
    }
}