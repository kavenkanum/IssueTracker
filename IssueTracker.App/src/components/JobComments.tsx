import React from "react";
import { Comment, Header, Form, Button, Container } from "semantic-ui-react";
import { useSelector } from "react-redux";
import { RootState } from "../store/root-reducer";
import { CommentReplay } from "./CommentReplay";
import moment from "moment";

export const JobComments = () => {
  const commentList = useSelector((state: RootState) => state.job.commentsList);
  const jobId = useSelector((state: RootState) => state.job.jobDetails?.jobId);

  return (
    <Container style={{ padding: "10px" }}>
      <Comment.Group threaded>
        <Header as="h3" dividing>
          Comments
        </Header>
        {commentList.map((c) => (
          <Comment>
            <Comment.Content>
              <Comment.Author as="a">FUTURE AUTHOR NAME</Comment.Author>
              <Comment.Metadata>
                <span>{moment(c.dateOfCreate).format("MMMM Do YYYY")}</span>
              </Comment.Metadata>
              <Comment.Text>{c.description}</Comment.Text>
              <Comment.Actions>
                <a>Reply</a>
              </Comment.Actions>
            </Comment.Content>
          </Comment>
        ))}
        <CommentReplay />
      </Comment.Group>
    </Container>
  );
};
