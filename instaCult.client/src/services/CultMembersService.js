import { AppState } from "../AppState.js"
import { Cultist } from "../models/Cultist.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class CultMembersService {
  async getCultMembers(cultId) {
    const res = await api.get(`api/cults/${cultId}/cultists`)
    logger.log('[GETTING CULTISTS]', res.data)
    AppState.cultists = res.data.map(c => new Cultist(c))
  }

  async createCultMember(cultId) {
    const res = await api.post('api/cultmembers', { cultId })
    logger.log('[CREATE CULT MEMBER]', res.data)
    const cultist = new Cultist(AppState.account)
    cultist.cultMemberId = res.data.id
    AppState.cultists.push(cultist)
  }

  async removeCultMember(cultMemberId) {
    const res = await api.delete('api/cultmembers/' + cultMemberId)
    logger.log('[REMOVING CULT MEMBER]', res.data)
    const index = AppState.cultists.findIndex(c => c.cultMemberId == cultMemberId)
    AppState.cultists.splice(index, 1)
  }

}

export const cultMembersService = new CultMembersService()