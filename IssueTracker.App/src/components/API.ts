import React, { useState, useEffect } from "react";
import { Project } from "../features/projects/slice";

enum Status {
  None,
  New,
  InProgress,
  Done,
}

export interface JobComment {
  description: string;
  userId: number;
  dateOfCreate: Date;
}

export interface NewJob {
  id: number;
  name: string;
  projectId: number;
}

export interface Job {
  jobId: number;
  name: string;
  descritpion: string;
  assignedUserId: number;
  status: Status;
  deadline: Date;
}

export const getProjects = (): Promise<Project[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch("https://localhost:5001/projects", {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

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

export const getJobs = (projectId: number): Promise<Job[]> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/projects/${projectId}/jobs`, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((response) => response.json());
};

export const getJob = (jobId: number): Promise<Job> => {
  const token = localStorage.getItem("accessToken");

  return fetch(`https://localhost:5001/jobs/${jobId}`, {
    headers: {
      Authorization: "Bearer " + token,
    },
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
