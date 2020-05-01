import { combineReducers } from "redux";
import { createReducer } from "typesafe-actions";
import { selectProject } from "./actions";

export type ProjectState = {
  id?: number;
  name?: string;
};

export const projectsReducer = createReducer<ProjectState>({
  id: 0,
}).handleAction(
  selectProject,
  (state: ProjectState, action) => ({
    ...state,
    ...action.payload,
  })
);

export default projectsReducer;
export type ProjectsState = ReturnType<typeof projectsReducer>;

// export const projectSelected = (state = 0, action: any) => {
//     switch(action.type) {
//         case 'SELECT_PROJECT':
//             return action.projectId;
//         default:
//             return state;
//     }
// }

// export default combineReducers({
//     projectSelected
// });
