import { Project } from "../features/projects/slice";
import jwt_decode from "jwt-decode";

export enum Priority {
  None,
  Low,
  Medium,
  High,
}

export enum Status {
  None,
  New,
  InProgress,
  Done,
}

export interface JobComment {
  description: string;
  userFullName: string;
  dateOfCreate: Date;
}

export interface NewJob {
  id: number;
  name: string;
  projectId: number;
}

export interface EditedJob {
  jobId: number;
  name: string;
  description: string;
  assignedUserId: number;
  deadline: Date;
  priority: Priority;
}

export interface Job {
  jobId: number;
  name: string;
  description: string;
  assignedUserID: number;
  status: Status;
  deadline: Date;
  priority: Priority;
}

export interface PreviousJob {
  prevJobId: number;
  name: string;
}

export const getProjects = (): Promise<Project[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch("https://localhost:5001/projects", {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const getProject = (projectId: number): Promise<Project> => {
  const token = localStorage.getItem("accessToken");
  
  return fetch(`https://localhost:5001/projects/${projectId}`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());

}

export const addProject = (name: string): Promise<number> => {
  const token = localStorage.getItem("accessToken");

  return fetch("https://localhost:5001/projects", {
    method: "POST",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ name }),
  }).then((response) => response.json());
};

export const getJobs = (
  projectId: number,
  jobsStatus: Status
): Promise<Job[]> => {
  const token = localStorage.getItem("accessToken");
const jobsStatusStringified = JSON.stringify({jobsStatus});
  return fetch(`https://localhost:5001/projects/${projectId}/jobs?jobsStatus=${jobsStatus}`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const getJob = (jobId: number, projectId: number): Promise<Job> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}?projectId=${projectId}`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const getEditJob = (jobId: number): Promise<EditedJob> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/edit`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const editJob = (
  jobId: number,
  name: string,
  description: string,
  assignedUserId: number,
  deadline: string,
  priority: number
): Promise<void> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/edit`, {
    method: "PUT",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      jobId,
      name,
      description,
      assignedUserId,
      deadline,
      priority,
    }),
  }).then((response) => response.json());
};

export const changeJobStatus = (
  jobId: number,
  requestedStatus: Status
): Promise<void> => {
  const token = localStorage.getItem("accessToken");
return fetch(`https://localhost:5001/jobs/${jobId}/changeJobStatus`, {
  method: "PATCH",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "application/json",
  },
  body: JSON.stringify({ jobId, requestedStatus }),
}).then((response) => response.json());

};

export const addJob = (projectId: number, jobName: string): Promise<number> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/projects/${projectId}/jobs`, {
    method: "POST",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ projectId, jobName }),
  }).then((response) => response.json());
};

export const addPrevJobs = (
  jobId: number,
  prevJobsId: number[]
): Promise<void> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/prevJobs`, {
    method: "POST",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ jobId, prevJobsId }),
  }).then((resp) => resp.json());
};

export const getPrevJobs = (jobId: number): Promise<PreviousJob[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/prevJobs`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const getComments = (jobId: number): Promise<JobComment[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/comments`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const addComment = (
  jobId?: number,
  description?: string
): Promise<number> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/comments`, {
    method: "POST",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ jobId, description }),
  }).then((response) => response.json());
};

export const getUsersFromProject = (projectId: number): Promise<User[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/${projectId}/users`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
}

export const getUsers = (jobId: number): Promise<User[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}/edit/getUsers`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export interface User {
  userId: number;
  fullName: string;
}
