import { createSlice, PayloadAction, createAsyncThunk } from "@reduxjs/toolkit";
import { AppThunk, history } from "../../store";
import { getJob, Job, JobComment, getComments, PreviousJob, getPrevJobs } from "../../components/API";

const initialState: JobState = {
  selectedJobId: 0,
  loading: false,
  commentsList: [],
  previousJobs: []
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
        requestFinished(state, action: PayloadAction<[Job, JobComment[], PreviousJob[]]>) {
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
        }
    }
}) 

export default slice;

export const loadJobDetails = (jobId: number): AppThunk => async (dispatch) => {
    dispatch(slice.actions.requestStarted());
    try {
      const jobDetails = await getJob(jobId);
      const commentsList = await getComments(jobId);
      const previousJobs = await getPrevJobs(jobId);
      dispatch(slice.actions.requestFinished([jobDetails, commentsList, previousJobs]));
    } catch (ex) {
      dispatch(slice.actions.requestFailed(ex.response));
    }
  };

export interface JobState {
    selectedJobId: number;
    loading: boolean;
    jobDetails?: Job;
    commentsList: JobComment[];
    previousJobs: PreviousJob[];
};

