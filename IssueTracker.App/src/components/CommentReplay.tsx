import React, { useState, FormEvent } from "react";
import { Form, Button, TextAreaProps } from "semantic-ui-react";
import { addComment } from "./API";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";

export const CommentReplay = () => {
    const jobId = useSelector((state: RootState) => state.job.jobDetails?.jobId);
    const [description, setDescription] = useState<any>("");
    const handleChange = (ev: FormEvent<HTMLTextAreaElement>, data: TextAreaProps) => setDescription(data.value?.toString());
    const addNewComment = () => {
        addComment(jobId, description).then(resp => resp);
    }
  return (
    <Form reply onSubmit={addNewComment}>
      <Form.TextArea value={description} onChange={handleChange}/>
      <Button content="Add Reply" labelPosition="left" icon="edit" primary />
    </Form>
  );
};
