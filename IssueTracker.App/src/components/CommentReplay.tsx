import React, { useState, FormEvent } from "react";
import { Form, TextAreaProps } from "semantic-ui-react";
import { addComment } from "./API";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import {CustomButton} from "./elements/CustomButton";

export const CommentReplay = () => {
    const jobId = useSelector((state: RootState) => state.job.jobDetails?.jobId);
    const [newCommentId, setNewCommentId] = useState<number>();
    const [description, setDescription] = useState<any>("");

    const handleChange = (ev: FormEvent<HTMLTextAreaElement>, data: TextAreaProps) => setDescription(data.value?.toString());
    const addNewComment = () => {
        addComment(jobId, description).then(resp => setNewCommentId(resp));
        window.location.reload();
    }
  return (
    <Form reply onSubmit={addNewComment}>
      <Form.TextArea value={description} onChange={handleChange}/>
      <CustomButton text="Add Reply" onClick={addNewComment}/>
    </Form>
  );
};
