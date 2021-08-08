class Metrics extends React.Component {    
    render() {
        return (
            <div>
                <h3>Метрики</h3>
                <RollingRetentionMetric retention={this.props.retention} />
                <LifetimeHistogram />
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
        const labels = ['2016', '2017', '2018'];
        const data = [324, 45, 672];
        const options = { fillColor: '#FFFFFF', strokeColor: '#0000FF' };
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