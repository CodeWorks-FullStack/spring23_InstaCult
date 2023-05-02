import { Profile } from "./Account.js";

export class Cultist extends Profile {
  constructor (data) {
    super(data)
    this.cultMemberId = data.cultMemberId
  }
}