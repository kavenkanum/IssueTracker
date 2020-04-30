import React, { useState, useEffect } from "react";

enum Status {
  None,
  New,
  InProgress,
  Done,
}

export interface Project {
  id: number;
  name: string;
}

export interface Job {
  id: number;
  name: string;
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
