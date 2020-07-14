import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk } from "../../store";
import { getProjects, getProject } from "../../components/API";

const initialState: ProjectState = {
  selectedProjectId: 0,
  selectedProjectName: "",
  loading: false,
};

const slice = createSlice({
  name: "project",
  initialState: initialState,
  reducers: {
    requestStarted(state) {
      state.loading = true;
    },
    requestFinished(state, action: PayloadAction<Project[]>) {
      state.loading = false;
      state.projectList = action.payload;
    },
    requestFailed(state, action: PayloadAction<any>) {
      state.loading = false;
      console.log(action.payload);
    },
    selectProject(state, action: PayloadAction<Project>) {
      state.loading = false;
      state.selectedProjectId = action.payload.id;
      state.selectedProjectName = action.payload.name;
    },
  },
});

export default slice;

export const loadProject = (projectId: number): AppThunk => async (
  dispatch
) => {
  dispatch(slice.actions.requestStarted());
  try {
    const project = await getProject(projectId);
    dispatch(slice.actions.selectProject(project));
  } catch (ex) {
    dispatch(slice.actions.requestFailed(ex.response));
  }
};

export const loadProjects = (): AppThunk => async (dispatch) => {
  dispatch(slice.actions.requestStarted());
  try {
    const projects = await getProjects();
    dispatch(slice.actions.requestFinished(projects));
  } catch (ex) {
    dispatch(slice.actions.requestFailed(ex.response));
  }
};

// export const registerAccount = (acc: AccountRegisterForm): AppThunk => async (
//   dispatch,
//   getState
// ) => {
//   dispatch(slice.actions.requestStarted());
//   dispatch(
//     setAccountDetails({
//       dateOfBirth: acc.dateOfBirth,
//       email: acc.email,
//       firstName: acc.firstName,
//       lastName: acc.lastName,
//     })
//   );

//   try {
//     await accClient.registerCandidate({
//       dateOfBirth: acc.dateOfBirth,
//       email: acc.email,
//       firstName: acc.firstName,
//       password: acc.password,
//       surname: acc.lastName,
//       marketingPreferences: acc.marketingPrefs,
//       gender: GenderType.NotSpecified,
//     });
//     dispatch(slice.actions.requestFinished());

//     GoogleTagManager.updateDataLayer({
//       marketingPreferences: marketingPrefsForGtm(acc.marketingPrefs),
//     });

//     setTimeout(() => {
//       history.push('/login');
//     }, 1000);
//   } catch (ex) {
//     dispatch(slice.actions.requestFailed(ex.response));
//   }
// };

/** Load info about candidate model connected to logged in user. If 404 then it means that there is no candidate */
export interface Project {
  id: number;
  name: string;
}

export interface ProjectState {
  selectedProjectId: number;
  selectedProjectName: string;
  projectList?: Array<Project>;
  loading: boolean;
}
