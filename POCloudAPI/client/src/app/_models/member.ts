import { filesObj } from "./filesObj";

export interface Member {
  id: number
  username: string
  created: Date
  lastLogin: Date
  files: filesObj[]
}
