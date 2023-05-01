import { Profile } from "./Account.js"


export class Cult {
  constructor(data) {
    this.name = data.name
    this.id = data.id
    this.description = data.description
    this.tags = data.tags.split(', ')
    this.leaderId = data.leaderId
    this.leader = new Profile(data.leader)
  }
}
