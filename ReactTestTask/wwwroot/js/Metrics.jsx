class Metrics extends React.Component {    
    render() {
        return (
            <div>
                <h3>Метрики</h3>
                <RollingRetentionMetric retention={this.props.retention} />
                <LifetimeHistogram distribution={this.props.distribution} />
            </div>
        );
    };
}

class RollingRetentionMetric extends React.Component {
    render() {
        return (
            <h3>Rolling retention: {this.props.retention}</h3>
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