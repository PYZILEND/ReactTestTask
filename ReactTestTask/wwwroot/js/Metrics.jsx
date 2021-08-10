class Metrics extends React.Component {
    constructor(props) {
        super(props);
        this.state = { retention : this.props.retention, distribution:this.props.distribution };
        this.onCalculateButtonClick = this.onCalculateButtonClick.bind(this);
    }

    onCalculateButtonClick() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.getMetricsUrl, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ retention: data.rollingRetention, distribution: data.lifetiemeDistribution });
        };
        xhr.send();
    }

    render() {
        return (
            <div>
                <h3>Метрики</h3>
                <button onClick={this.onCalculateButtonClick}>Calculate</button>
                <RollingRetentionMetric retention={this.state.retention} />
                <LifetimeHistogram distribution={this.state.distribution} />
            </div>
        );
    };
}

class RollingRetentionMetric extends React.Component {
    render() {
        return (
            <h4>Rolling retention 7 day: {this.props.retention}%</h4>
        );
    }
}

class LifetimeHistogram extends React.Component {
    render() {
        const labels = Object.keys(this.props.distribution);
        const data = this.props.distribution;
        const options = { fillColor: '#0000FF', strokeColor: '#0000FF' };
        return (
            <div>
                <h4>Распределение длительностей жизней пользователей</h4>
                <Histogram
                    xLabels={labels}
                    yValues={data}
                    width='400'
                    height='200'
                    options={options}
                />
            </div>
        )
    }
}