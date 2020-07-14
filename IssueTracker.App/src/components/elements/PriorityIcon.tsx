import React from "react";
import { Priority } from "../API";
import { Image } from "semantic-ui-react";

// medium priority https://ibb.co/DDYTVVP

// low priority https://ibb.co/v3xgJzL

const jobsIconsStyle = {
   
}

export const PriorityIcon = (props: any) => {
  const iconImage = (priority: Priority) => {
    switch (priority) {
      case Priority.Low:
        return <Image
        src="https://i.ibb.co/X7ZNzJF/icons8-low-priority-100.png"
        inline
        size="mini"
        style={jobsIconsStyle}
      />
      case Priority.Medium:
        return <Image
      src="https://i.ibb.co/3yvQrr5/icons8-medium-priority-100.png"
      inline
      size="mini"
      style={jobsIconsStyle}
    />
      case Priority.High:
        return <Image
        src="https://img.icons8.com/plasticine/100/000000/high-priority.png"
        inline
        size="mini"
        style={jobsIconsStyle}
      />
      default:
        return <div></div>
    }
  };
  return iconImage(props.priority);
};
