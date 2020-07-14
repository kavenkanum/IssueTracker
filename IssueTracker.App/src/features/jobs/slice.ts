import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk } from "../../store";
import {
  getJob,
  Job,
  JobComment,
  getComments,
  PreviousJob,
  getPrevJobs,
} from "../../components/API";
import { push } from "connected-react-router";

const initialState: JobState = {
  selectedJobId: 0,
  loading: false,
  commentsList: [],
  previousJobs: [],
};

const slice = createSlice({
  name: "job",
  initialState: initialState,
  reducers: {
    selectJob(state, action: PayloadAction<number>) {
      state.selectedJobId = action.payload;
    },
    requestStarted(state) {
      state.loading = true;
    },
    requestFinished(
      state,
      action: PayloadAction<[Job, JobComment[], PreviousJob[]]>
    ) {
      state.selectedJobId = action.payload[0].jobId;
      state.jobDetails = action.payload[0];
      state.commentsList = action.payload[1];
      state.previousJobs = action.payload[2];
      state.loading = false;
    },
    requestFailed(state, action: PayloadAction<any>) {
      state.loading = false;
      console.log(action.payload);
    },
    removeSelectedJob(state) {
      state.selectedJobId = initialState.selectedJobId;
      state.commentsList = initialState.commentsList;
    },
  },
});

export default slice;

export const loadJobDetails = (
  jobId: number,
  projectId: number
): AppThunk => async (dispatch) => {
  dispatch(slice.actions.requestStarted());
  try {
    const jobDetails = await getJob(jobId, projectId);
    const commentsList = await getComments(jobId);
    const previousJobs = await getPrevJobs(jobId);
    dispatch(
      slice.actions.requestFinished([jobDetails, commentsList, previousJobs])
    );
  } catch (ex) {
    dispatch(slice.actions.requestFailed(ex.response));
    dispatch(push(`/dashboard/${projectId}`));
  }
};

export interface JobState {
  selectedJobId: number;
  loading: boolean;
  jobDetails?: Job;
  commentsList: JobComment[];
  previousJobs: PreviousJob[];
}
