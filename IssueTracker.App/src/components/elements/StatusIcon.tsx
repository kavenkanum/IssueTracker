import React from "react";
import { Status, Priority } from "../API";
import { Container, Header, Button, Image, Dropdown } from "semantic-ui-react";

//status new https://i.ibb.co/p4PH1K7/icons8-survey-100.png
// status inprogress https://img.icons8.com/plasticine/100/000000/task.png
//status done https://img.icons8.com/plasticine/100/000000/task-completed.png
const jobsIconsStyle = {

}

export const StatusIcon = (props: any) => {
    const iconImage = (status: Status) => {
        switch(status) {
            case Status.New:
                return <Image
                src="https://i.ibb.co/p4PH1K7/icons8-survey-100.png"
                inline
                size="mini"
                style={jobsIconsStyle}
              />
              case Status.InProgress:
                return <Image
                src="https://img.icons8.com/plasticine/100/000000/task.png"
                inline
                size="mini"
                style={jobsIconsStyle}
              />
              case Status.Done:
                return <Image
                src="https://img.icons8.com/plasticine/100/000000/task-completed.png"
                inline
                size="mini"
                style={jobsIconsStyle}
              />
            default:
                return <div></div>
        }
    };
    return iconImage(props.status);
};
