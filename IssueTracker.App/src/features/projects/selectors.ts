import { RootState } from "typesafe-actions";

export const getProjectName = (state: RootState) => state.project.name;
export const getProjectId = (state: RootState) => state.project.id;
