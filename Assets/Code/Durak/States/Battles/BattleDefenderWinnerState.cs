﻿
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using ProjectCard.Durak.CardModule;
using ProjectCard.Durak.EntityModule;
using ProjectCard.Durak.PlayerModule;
using ProjectCard.Durak.ServiceModule.MovementModule;
using ProjectCard.Shared.ViewModule;

using UnityEngine;


namespace ProjectCard.Durak.StateModule.BattleModule
{
    public class BattleDefenderWinnerState : BattlePlayerWinnerState
    {
        [Header(nameof(BattleDefenderWinnerState))]
        [Header("Movement")]
        [SerializeField] private DurakCardMovementManager movement;

        [Header("Entities")]
        [SerializeField] DiscardPileEntity discardPile;

        [Header("Places")]
        [SerializeField] private Transform trashPlace;

        protected override async UniTask MoveCards(IReadOnlyList<Data> datas)
        {
            discardPile.Value.AddRange(datas);

            await movement.MoveToPlace(datas, trashPlace, CardLookSide.Back);

        }
        protected override void UpdatePlayerQueue(IPlayerQueue playerQueue)
        {
            playerQueue.Set(
                attacker: playerQueue.Defender,
                defender: playerQueue.GetNextFrom(playerQueue.Defender),
                action: PlayerActionType.Attack);
        }
    }
}