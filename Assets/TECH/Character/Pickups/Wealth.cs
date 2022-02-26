/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Character.Abilities
{
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// The Wealth Class
    /// More Wealth Gives you More Movement Speed
    /// </summary>
    [AllowDuplicateTypes]
    [DefaultStartType(AbilityStartType.Automatic)]
    [DefaultStopType(AbilityStopType.Automatic)]
    public class Wealth : Ability
    {

        [Tooltip("The wealth multiplier for the speed.")]
        [SerializeField] protected float m_WealthMultiplier = 1;
        [Tooltip("The minimum value the WealthMultiplier can change the InputVector value to.")]
        [SerializeField] protected float m_MinWealthValue = -2;
        [Tooltip("The maximum value the WealthMultiplier can change the InputVector to.")]
        [SerializeField] protected float m_MaxWealthValue = 2;
        [Tooltip("Does the ability require movement in order to stay active?")]
        [SerializeField] protected bool m_RequireMovement = true;

        public float WealthMultiplier { get { return m_WealthMultiplier; } set { m_WealthMultiplier = value; } }
        public float MinWealthValue { get { return m_MinWealthValue; } set { m_MinWealthValue = value; } }
        public float MaxWealthValue { get { return m_MaxWealthValue; } set { m_MaxWealthValue = value; } }
        public bool RequireMovement { get { return m_RequireMovement; } set { m_RequireMovement = value; } }

        public override bool IsConcurrent { get { return true; } }

        /// <summary>
        /// Called when the ablity is tried to be started. If false is returned then the ability will not be started.
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            // An attribute may prevent the ability from starting.
            if (!base.CanStartAbility())
            {
                return false;
            }

            return !m_RequireMovement || m_CharacterLocomotion.Moving;
        }

        /// <summary>
        /// Should the input be checked to ensure button up is using the correct value?
        /// </summary>
        /// <returns>True if the input should be checked.</returns>
        protected override bool ShouldCheckInput() { return false; }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {

            base.AbilityStarted();
        }

        /// <summary>
        /// Updates the ability. Applies a movement multiplier.
        /// </summary>
        public override void Update()
        {
            Debug.Log("Entered");

            base.Update();

            // If RequireMovement is true then the character must be moving in order for the ability to be active.
            if (m_RequireMovement && !m_CharacterLocomotion.Moving)
            {
                StopAbility(true);
                return;
            }

            var inputVector = m_CharacterLocomotion.InputVector;
            inputVector.x = Mathf.Clamp(inputVector.x * m_WealthMultiplier, m_MinWealthValue, m_MaxWealthValue);
            inputVector.y = Mathf.Clamp(inputVector.y * m_WealthMultiplier, m_MinWealthValue, m_MaxWealthValue);
            m_CharacterLocomotion.InputVector = inputVector;

            // The raw input vector should be updated as well. This allows other abilities to know if the character has a different speed.
            inputVector = m_CharacterLocomotion.RawInputVector;
            inputVector.x = Mathf.Clamp(inputVector.x * m_WealthMultiplier, m_MinWealthValue, m_MaxWealthValue);
            inputVector.y = Mathf.Clamp(inputVector.y * m_WealthMultiplier, m_MinWealthValue, m_MaxWealthValue);
            m_CharacterLocomotion.RawInputVector = inputVector;
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

        }
    }
}