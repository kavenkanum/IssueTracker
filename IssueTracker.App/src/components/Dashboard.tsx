import React, {useState, useEffect} from "react";
import { Container } from 'semantic-ui-react';
import { useSelector } from "react-redux";
import { getUserToken } from "../features/users/selectors";
import { render } from "react-dom";

interface JobDto {
    jobId: number;
    name: string;
    description: string;
    assignedUserId: string;
    deadline: Date;
    dateOfCreate: Date;
}

export const Dashboard: React.FC = () => {
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState<any>();
    const token = localStorage.getItem('accessToken');
    
//     return <Container>
//     <p>token: {token} </p>
// </Container>
// }
    useEffect(() => {
      setLoading(true);
      fetch('https://localhost:5001/HealthMonitor', {
        headers: {
          'Authorization': 'Bearer ' + token,
        }
      })
        .then((data) => {
            debugger;
          setData(data);
        })
        .catch((err) => {
          console.log('Error!', err);
        })
        .finally(() => setLoading(false));
    }, []);
  
    if (loading) {
      return <div>Loading</div>;
    }
  
    return (
      <div>
          <ul>
            Token: {token}
            Data: {data.data}
          </ul>
      </div>
    );
};  
