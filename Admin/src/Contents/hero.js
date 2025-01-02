import React, { useEffect, useState } from 'react';
import { getData } from '../Services/apiService';


const Hero = () => {
    const [data, setData] = useState([]);

    useEffect(() => {
        getData('/your-endpoint')
            .then((response) => setData(response.data))
            .catch((error) => console.error(error));
    }, []);

    return (
        <div>
            <h1>Hello</h1>
            {/* <pre>{JSON.stringify(data, null, 2)}</pre> */}
            <a>this is the sample Hero Page</a>
        </div>
    );
};

export default Hero;
