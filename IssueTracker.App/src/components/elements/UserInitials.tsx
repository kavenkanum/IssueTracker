import React from "react";
import { Label } from "semantic-ui-react";

const colors: any = [
    'red',
    'yellow',
    'green',
    'teal',
    'blue',
    'violet',
    'purple',
    'pink',
    'grey',
    'black'
  ];  

export const UserInitials = (props: any) => {
  const createInitials = (fullName: string): string => {
    return fullName.match(/[A-Z]/g)!.join("");
  };
  return (
      <Label circular color={colors[Math.floor(Math.random()*10)]} size="large" >{createInitials(props.fullName)}</Label>
  );
};
